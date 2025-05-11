/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { Car } from "./car";

export interface Maintenance {
    id: number;
    carId: number;
    car: Car;
    type: string;
    description: string;
    cost: number;
    date: Date;
}
