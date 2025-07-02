import { createApp } from 'vue'
import App from './App.vue'
//引入全局样式
//import './style.css' 不使用项目创建的默认样式
import '@/css/global.scss'
//引入路由
import router from './router'

const app = createApp(App)

app.use(router)
app.mount('#app')