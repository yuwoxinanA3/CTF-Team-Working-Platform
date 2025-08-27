import { defineStore } from 'pinia';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  sub: string;     // 对应后端的 userId
  id: string;      // 对应后端的 id
  jti: string;     // JWT ID
  exp: number;     // 过期时间
  iat: number;     // 签发时间
  iss?: string;    // 签发者
  aud?: string;    // 接收者
  [key: string]: any;
}

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || null,
  }),
  actions: {
    setToken(token: string) {
      this.token = token;
      localStorage.setItem('token', token); // 持久化存储
    },
    clearToken() {
      this.token = null;
      localStorage.removeItem('token');
    },
    getUserId(): string | null {
      if (!this.token) return null;
      
      try {
        const decoded: JwtPayload = jwtDecode(this.token);
        return decoded.id || null; // id 字段对应后端的 id
      } catch (error) {
        console.error('Token解析失败:', error);
        return null;
      }
    },
  },
});