import { CartItem } from "../cart/cartItem";

export interface Order {
    id: number,
    totalPrice: number,
    shopId: number,
    orderDate: Date,
    orderLines: CartItem[]
}
