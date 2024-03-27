export interface TokenModel {
    roles: string[];
    id: string;
    token: string;
    expiry: string;
    expiryInMilliSecond: number;
    userName: string;
    email: string;
    profileImage: string;
}