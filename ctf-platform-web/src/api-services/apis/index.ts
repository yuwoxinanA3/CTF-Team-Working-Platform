// api/index.ts 或类似文件
import axios from 'axios';
import { useAuthStore } from '@/store/authStore';

// 创建axios实例
const apiClient = axios.create({
  baseURL: '/api', // 设置基础URL
});

// 请求拦截器
apiClient.interceptors.request.use(
  (config) => {
    // 确保在客户端环境中运行
    if (typeof window !== 'undefined') {
      const authStore = useAuthStore();
      const token = authStore.token;
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default apiClient;