/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { Car } from "./car";

export interface Insurance {
    id: number;
    carId: number;
    car: Car;
    policyNumber: string;
    companyName: string;
    insuranceType: string;
    issueDate: Date;
    expirationDate: Date;
    fileUrl: string;
}
