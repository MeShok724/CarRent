/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { User } from "./user";
import { Car } from "./car";
import { Branch } from "./branch";
import { Employee } from "./employee";
import { Discount } from "./discount";

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
