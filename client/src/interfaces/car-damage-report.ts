/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { Car } from "./car";
import { Employee } from "./employee";

export interface CarDamageReport {
    id: number;
    carId: number;
    car: Car;
    employeeId: number;
    employee: Employee;
    reportDate: Date;
    description: string;
    damageType: string;
    photoUrl: string;
    isResolved: boolean;
    resolvedAt: Date;
}
