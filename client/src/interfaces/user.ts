
import type { UserStatus } from "./user-status";

export interface User {
    id: number;
    username: string;
    email: string;
    passwordHash: string;
    createTime: Date;
    statusId: number;
    status: UserStatus;
}
