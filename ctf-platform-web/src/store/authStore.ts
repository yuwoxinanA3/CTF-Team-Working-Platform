/**
 * JWT认证状态管理
 */
import { defineStore } from 'pinia';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  sub: string;     // 对应后端的 userId（主要用户标识）
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
    /**
     * 存储token
     * @param {string} token - 待设置的token
     */
    setToken(token: string) {
      this.token = token;
      localStorage.setItem('token', token); // 持久化存储
    },
    /**
     * 清除token
     */
    clearToken() {
      this.token = null;
      localStorage.removeItem('token');
    },
    /**
     * 解析token,获取用户ID
     * @returns {string | null} 用户ID
     */
    getUserId(): string | null {
      if (!this.token) return null;

      try {
        const decoded: JwtPayload = jwtDecode(this.token);
        return decoded.sub || null;
      } catch (error) {
        console.error('Token解析失败:', error);
        return null;
      }
    },
  },
});