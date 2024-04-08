export interface AuthResponse {
  token: string;
  refreshToken: string;
  expiration: string;
  isSuccess: true;
  loggedUser: string;
  userName: string;
  userId: string;
}
