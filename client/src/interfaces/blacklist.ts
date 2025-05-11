/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import type { User } from "./user";

export interface Blacklist {
    id: number;
    userId: number;
    user: User;
    reason: string;
    bunnedAt: Date;
    expirationDate: Date;
}
