import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { interstRates } from '../_models/interestRates';
import { repaymentInput } from '../_models/repaymentInput';
import { repaymentPlan } from '../_models/repaymentPlan';
import { CalcService } from '../_services/calc.service';

@Component({
  selector: 'app-calculations',
  templateUrl: './calculations.component.html',
  styleUrls: ['./calculations.component.css']
})
export class CalculationsComponent implements OnInit {

  loanType: interstRates = {
    id: 0,
    loanType:'',
    interestRate: 0
  };

  repaymentInput: repaymentInput;
  repaymentPlan: repaymentPlan[];

  calculationForm: FormGroup;

  columnDefs = [
    { field: 'month' },
    { field: 'installment'}
  ];

  rowData = [];

  constructor(
    private calcService: CalcService,
    private activatedRoute: ActivatedRoute
  ) {
    this.calculationForm = new FormGroup({
      loanAmount: new FormControl('', [Validators.required, Validators.maxLength(8), Validators.pattern("^[0-9]*$")]),
      installmentsYears: new FormControl('',[Validators.required, Validators.maxLength(2), Validators.pattern("^[0-9]*$")])
    });

   }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(
      paramMap => {
        this.loanType.id = +paramMap.get('id');
      }
    );
    this.calcService.getLoanInfo(this.loanType.id).subscribe(
      response => {
        this.loanType = response;
      }
    );
  }

  onSubmit(){
    this.repaymentInput = {
      installmentsYears: +this.installmentsYears.value,
      loanAmount: +this.loanAmount.value,
      interestRate: this.loanType.interestRate
    };
    
    this.calcService.getRepaymentPlan(this.repaymentInput)
      .subscribe(
        response => {
          this.repaymentPlan = response;

          this.rowData = this.repaymentPlan;
        }
      );
  }
  

  get loanAmount() {
    return this.calculationForm.get('loanAmount');
  }

  get installmentsYears() {
    return this.calculationForm.get('installmentsYears');
  }

}
