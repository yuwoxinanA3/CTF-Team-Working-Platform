import { defineStore } from 'pinia';

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
  },
});