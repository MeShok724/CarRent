/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { Order } from "./order";

export interface Discount {
    id: number;
    name: string;
    isActive: boolean;
    orders: Order[] = [];
}
