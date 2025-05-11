import type { User } from "./user";

export interface UserStatus {
    id: number;
    name: string;
    users: User[];
}
