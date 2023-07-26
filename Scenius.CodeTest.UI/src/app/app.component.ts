import { Component } from '@angular/core';
import { CalculatorComponent } from './calculator/calculator.component';
import { HttpClient } from '@angular/common/http';
import { CalculationComponent } from './calculation/calculation.component';
import { ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class AppComponent {
  title = 'scenius-codetest';
  hallo : Calculation[];

  constructor(private _http : HttpClient) {
    this.hallo = [];
  }

  function() {
    this._http.get('http://localhost:5000/ingest').subscribe(result => {
      this.hallo = <Array<Calculation>>result;
    });
  }
}
export interface Calculation {
  id: Number;
  input: string;
  result: Number;
}
