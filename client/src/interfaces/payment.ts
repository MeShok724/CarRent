/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { Order } from "./order";
import { User } from "./user";
import { PaymentType } from "./payment-type";

export interface Payment {
    id: number;
    orderId: number;
    order: Order;
    customerId: number;
    customer: User;
    amount: number;
    paymentTypeId: number;
    paymentType: PaymentType;
    paymentDate: Date;
}
