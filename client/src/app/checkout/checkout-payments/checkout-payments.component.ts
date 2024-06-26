import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/models/basket';
import { Address } from 'src/app/shared/models/user';
import { NavigationExtras, Route, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payments',
  templateUrl: './checkout-payments.component.html',
  styleUrls: ['./checkout-payments.component.scss'],
})
export class CheckoutPaymentsComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,    
    private router: Router
  ) {}

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    if (!basket) return;
    const orderToCreate = this.getOrderToCreate(basket);
    if(!orderToCreate) return;
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: order=>{
        this.toastr.success('Order Created successfully');
        this.basketService.deleteLocalBasket();
        const navigationExtras: NavigationExtras={state: order};
        this.router.navigate(['checkout/success'], navigationExtras);
      }
    })
  }
  private getOrderToCreate(basket: Basket) {
    const deliveryMethodId = this.checkoutForm
      ?.get('deliveryForm')
      ?.get('deliveryMethod')?.value;
      const shipToAddress=this.checkoutForm?.get('addressForm')?.value as Address;
      if(!deliveryMethodId || !shipToAddress) return;
      return{
        basketId:basket.id,
        deliveryMethodId: deliveryMethodId,
        shipToAddress:shipToAddress
      }
  }
}
