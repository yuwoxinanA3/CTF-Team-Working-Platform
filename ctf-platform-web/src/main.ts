import { createApp } from 'vue'
import App from './App.vue'
//引入全局样式
//import './style.css' 不使用项目创建的默认样式
import '@/css/global.scss'
//引入elementUI plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
//引入路由
import router from './router'
//引入pinia
import { createPinia } from 'pinia'

const app = createApp(App)
//使用饿了么UI
app.use(ElementPlus)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}
//挂载路由
app.use(router)
// 挂载 Pinia 
const pinia = createPinia()
app.use(pinia)
app.mount('#app')