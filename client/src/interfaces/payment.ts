

import type { Order } from "./order";
import type { User } from "./user";
import type { PaymentType } from "./payment-type";

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
