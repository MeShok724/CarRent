/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserStatus } from "./user-status";

export interface User {
    id: number;
    username: string;
    email: string;
    passwordHash: string;
    createTime: Date;
    statusId: number;
    status: UserStatus;
}
