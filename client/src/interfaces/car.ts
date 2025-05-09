
import type { CarCategory } from "./car-category";
import type { CarStatus } from "./car-status";
import type { Branch } from "./branch";
import type { Order } from "./order";
import type { CarImage } from "./car-image";

export interface Car {
    id: number;
    brand: string;
    model: string;
    year: number;
    vin: string;
    licensePlate: string;
    color: string;
    mileage: number;
    categoryId: number;
    category: CarCategory;
    statusId: number;
    status: CarStatus;
    branchId: number;
    branch: Branch;
    rentalPricePerDay: number;
    engineVolume: number;
    seats: number;
    addedAt: Date;
    updatedAt: Date;
    orders: Order[];
    carImages: CarImage[];
}
