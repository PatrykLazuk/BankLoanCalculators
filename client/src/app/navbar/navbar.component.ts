import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { interstRates } from '../_models/interestRates';
import { CalcService } from '../_services/calc.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  loanTypes: interstRates[];

  constructor(
    private calcService: CalcService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getLoanCategories();
  }

  getLoanCategories(){
    this.calcService.getLoanTypes()
      .subscribe(
        response => {
          this.loanTypes = response;
        }
      );
  }

  goToPage(category: string) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['calculations/', category]));
  }

}
