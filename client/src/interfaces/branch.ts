/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { User } from "./user";

export interface Branch {
    id: number;
    name: string;
    city: string;
    address: string;
    postalCode: string;
    phone: string;
    managerId: number;
    manager: User;
}
