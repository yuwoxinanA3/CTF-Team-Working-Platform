import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'index',
            component: () => import('@/views/admin/user.vue')
        },
        {
            path: '/login',
            name: 'login',
            component: () => import('@/views/index/login.vue')
        },
        {
            path: '/:pathMatch(.*)',
            name: 'notfund',
            component: () => import('@/views/index/404.vue')
        }
    ]
})

export default router; 