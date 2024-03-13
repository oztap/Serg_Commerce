import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'SkiNet';

  constructor(
    private basketService: BasketService,
    private accountSevice: AccountService
  ) {}

  ngOnInit(): void {
   this.loadBasket();
   this.loadCurrentUser();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) this.basketService.getBasket(basketId);
  }

  loadCurrentUser() {
    const token = localStorage.getItem('z;!v]~R`>u&Wtjbw=q*GV~8e*+<;s2]NFCUSP=%3+q&g{ux3NYM5H5G,x~j*h{WK');
    this.accountSevice.loadCurrentUser(token).subscribe();
  }
}
