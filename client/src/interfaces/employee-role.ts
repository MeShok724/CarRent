/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { Employee } from "./employee";

export interface EmployeeRole {
    id: number;
    name: string;
    description: string;
    employees: Employee[] = [];
}
