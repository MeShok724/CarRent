/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { EmployeeRole } from "./employee-role";
import type { Branch } from "./branch";
import type { Order } from "./order";

export interface Employee {
    id: number;
    roleId: number;
    role: EmployeeRole;
    branchId: number;
    branch: Branch;
    firstname: string;
    lastname: string;
    email: string;
    phone: string;
    orders: Order[] = [];
}
