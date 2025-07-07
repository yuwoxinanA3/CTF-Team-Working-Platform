import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),//使用@代表根路径
      '@components': path.resolve(__dirname, './src/components')
    }
  },
  server: {
    //启动项目后自动打开浏览器
    open: true,
    //设置主机
    host: '127.0.0.1',
    //端口
    port: 5001,
    //代理
    proxy: {
      '/api': {
        target: 'http://localhost:5193',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, '')
      }
    }
  },
})
