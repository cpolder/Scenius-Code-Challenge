using Scenius.CodeTest.API.Exceptions;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.API.Repositories;
using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.Serialization;

namespace Scenius.CodeTest.API.Services
{
    public class IngestService : IIngestService
    {

        private readonly IIngestRepository _calculatorRepository;
        private readonly IProducer _producer;

        public IngestService(IIngestRepository ingestRepository, IProducer producer) 
        {
            _calculatorRepository = ingestRepository;
            _producer = producer;
        }

        // Retrieves all calculations from the database
        public IEnumerable<Calculation> getCalculations()
        {
            return _calculatorRepository.getAllCalculations();
        }

        // Generates a random number as the answer to a calculation and puts the combination on the message queue
        public async void performCalculation(Calculation calculation, bool generated)
        {
            // TODO: Replace Random number generator with calculation
            calculation.Result = calculateResult(calculation);

            await _producer.ExecuteAsync(new CancellationToken(), calculation);
        }

        // Calculates the result of a calculation
        // Process: first take the + and - operators.
        // Then Split the calculation input string into parts based on these operators.
        // Then calculate an individual part using a recursive method.
        // Afterwards do the add or substract operation on that part on the already existing result.
        // Repeat this for each part 
        // This ensures order of operations
        private int calculateResult(Calculation calculation)
        {
            int result = 0;
            string formula = calculation.Input;
            List<char> plusminusops = new List<char>();
            plusminusops.Add( '+' );
            formula.ToList().ForEach(x =>
            {
                if (char.IsLetter(x))
                {
                    throw new InputException();
                }
                if (x.Equals('+') || x.Equals('-'))
                {
                    plusminusops.Add(x);
                }

            });
            string[] plusminussplit = formula.Split(new char[] { '+', '-' }, StringSplitOptions.TrimEntries);
            foreach (string s in plusminussplit)
            {
                List<string> digits = new List<string>();
                List<char> ops = new List<char>();
                s.ToList().ForEach(x =>
                {
                    if (x.Equals('*') || x.Equals('/'))
                    {
                        ops.Add(x);
                    }
                });
                digits.AddRange(s.Split(new char[] { '*', '/' }));
                List<int> ints = new List<int>();
                digits.ForEach(x =>
                {
                    ints.Add(int.Parse(x));
                });
                if (plusminusops.Count == 0)
                {
                    result = calculateMult(ints, ops)[0];
                }
                else if (plusminusops[0].Equals('+'))
                {
                    plusminusops.RemoveAt(0);
                    result += calculateMult(ints, ops)[0];
                }
                else
                {
                    plusminusops.RemoveAt(0);
                    result -= calculateMult(ints, ops)[0];
                }
            }
            return result;
            
        }

        // A recursive method to calculate the result of a part with only * and / operators.
        // It takes the first two digits, and handles the operation on these.
        // Afterwards it replaces the digits with the result, and removes the original two digits and operator.
        // Then it calls itself until only 1 digit is left: the result.
        private List<int> calculateMult(List<int> digits, List<char> operators)
        {
            List<int> newDigits = new List<int>();
            if(digits.Count==1)
            {
                return digits;
            } 
            else if (operators[0].Equals('*')) 
            {
                int number1 = digits[0];
                int number2 = digits[1];
                digits.RemoveAt(0);
                digits.RemoveAt(0);
                operators.RemoveAt(0);
                digits.Insert(0, number1 * number2);
                newDigits.Add(calculateMult(digits, operators)[0]);
            } 
            else if (operators[0].Equals('/'))
            {
                int number1 = digits[0];
                int number2 = digits[1];
                digits.RemoveAt(0);
                digits.RemoveAt(0);
                operators.RemoveAt(0);
                digits.Insert(0, number1 / number2);
                newDigits.Add(calculateMult(digits, operators)[0]);
            }
            return newDigits;
        }

    }
}
