/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { Car } from "./car";
import type { User } from "./user";
import type { Order } from "./order";

export interface CarReview {
    id: number;
    carId: number;
    car: Car;
    customerId: number;
    customer: User;
    orderId: number;
    order: Order;
    rating: number;
    reviewText: string;
    addedAt: Date;
}
