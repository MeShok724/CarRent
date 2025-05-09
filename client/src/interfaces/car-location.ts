/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { Car } from "./car";

export interface CarLocation {
    id: number;
    carId: number;
    car: Car;
    latitude: number;
    longitude: number;
    updatedAt: Date;
}
