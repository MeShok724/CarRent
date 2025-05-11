/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { User } from "./user";
import type { Car } from "./car";
import type { Branch } from "./branch";
import type { Employee } from "./employee";
import type { Discount } from "./discount";

export interface Order {
    id: number;
    customerId: number;
    customer: User;
    carId: number;
    car: Car;
    branchFromId: number;
    branchFrom: Branch;
    branchToId: number;
    branchTo: Branch;
    employeeId: number;
    employee: Employee;
    startDate: Date;
    returnDate: Date;
    priceTotal: number;
    discountId: number;
    discount: Discount;
    status: string;
    notes: string;
    createdAt: Date;
}
