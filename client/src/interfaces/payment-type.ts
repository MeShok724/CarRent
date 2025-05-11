
import type { Payment } from "./payment";

export interface PaymentType {
    id: number;
    name: string;
    payments: Payment[];
}
