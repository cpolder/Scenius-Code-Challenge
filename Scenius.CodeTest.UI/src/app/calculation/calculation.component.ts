import { Component } from '@angular/core';

@Component({
  selector: 'app-calculation',
  templateUrl: './calculation.component.html',
  styleUrls: ['./calculation.component.scss']
})
export class CalculationComponent {
  input: String;
  result: Number;

  constructor(input: String, result: Number) {
    this.input = input;
    this.result = result;
  }

  
}
