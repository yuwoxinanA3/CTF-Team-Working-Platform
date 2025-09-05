// api/index.ts 或类似文件
import axios from 'axios';
import { useAuthStore } from '@/store/authStore';
import router from '@/router';

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

// 响应拦截器 - 添加 token 过期处理
apiClient.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    // 处理 401 未授权错误
    if (error.response?.status === 401) {
      // 清除认证信息并跳转到登录页
      const authStore = useAuthStore();
      authStore.clearToken();
      // 跳转到登录页面
      router.push('/login')
    }

    return Promise.reject(error);
  }
);

export default apiClient;