import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'index',
            component: () => import('@/views/index/login.vue')
        },
        {
            path: '/login',
            name: 'login',
            component: () => import('@/views/index/login.vue')
        },
        {
            path: '/:pathMatch(.*)',
            name: 'notfund',
            component: () => import('@/views/index/notFund.vue')
        }
    ]
})

export default router; 