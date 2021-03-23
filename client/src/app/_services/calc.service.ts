import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { interstRates } from '../_models/interestRates';
import { repaymentInput } from '../_models/repaymentInput';
import { repaymentPlan } from '../_models/repaymentPlan';

@Injectable({
  providedIn: 'root'
})
export class CalcService {

  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  getLoanTypes(): Observable<interstRates[]> {

    return this.http.get<interstRates[]>(this.baseUrl + "/Calculations");
  }

  getLoanInfo(id: number): Observable<interstRates> {
    return this.http.get<interstRates>(this.baseUrl + "/Calculations/" + id);
  }

  getRepaymentPlan(repaymentInput: repaymentInput): Observable<repaymentPlan[]>
  {
    return this.http.post<repaymentPlan[]>(this.baseUrl + "/Calculations/createrepaymentplan", repaymentInput);
  }
}
