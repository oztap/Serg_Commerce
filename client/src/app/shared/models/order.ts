import { Address } from "./user";

export interface orderToCreate {
    basketId: string;
    deliveryMethodId: number;
    shipToAddress: Address;
    
  }
  export interface orderItem {
    productId: number;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
  }
  export interface order {
    id: number;
    buyerEmail?: any;
    orderDate: string;
    shipToAdress: Address;
    deliveryMethod: string;
    shippingPrice: number;
    orderItems: orderItem[];
    subtotal: number;
    total: number;
    status: string;
  }