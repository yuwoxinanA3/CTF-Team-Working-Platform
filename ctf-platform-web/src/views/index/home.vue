<template>
    <el-menu :default-active="activeIndex" class="el-menu-demo nav-box" mode="horizontal" :ellipsis="false"
        @select="handleSelect">
        <el-menu-item index="0">
            <img style="width: 50px" src="@/assets/images/主页贴图.png" alt="Rotten logo" />
            <span style="font-size: x-large;">CTF团队协作平台</span>
        </el-menu-item>
        <el-menu-item index="1" class="nav-font">首页</el-menu-item>

        <el-menu-item index="2" class="nav-font">赛事中心</el-menu-item>

        <el-menu-item index="3" class="nav-font">训练中心</el-menu-item>
        <el-menu-item index="4" class="nav-font">战队中心</el-menu-item>


        <el-sub-menu index="5">
            <template #title>
                <div class="nav-font nav-font-submenu">资料中心</div>
            </template>
            <el-menu-item index="5-1">博客平台</el-menu-item>
            <el-menu-item index="5-2">视频平台</el-menu-item>
        </el-sub-menu>

        <el-sub-menu index="6">
            <template #title>
                <div class="nav-font nav-font-submenu">工具中心</div>
            </template>
            <el-menu-item index="6-1">在线工具</el-menu-item>
            <el-menu-item index="6-2">工具下载</el-menu-item>
            <el-menu-item index="6-3">工具指南</el-menu-item>
        </el-sub-menu>


        <div class="theme-toggle">
            中
        </div>

        <div class="theme-toggle" @click="toggleTheme">
            <el-icon size="20px">
                <component :is="isDarkTheme ? 'Moon' : 'Sunny'" />
            </el-icon>
        </div>

        <el-menu-item index="7" class="nav-font" @click="showUserPanel">
            <el-avatar :size="40"
                :src="'http://localhost:5193/uploads/avatars/7d4e03c9-ba2d-49f6-9428-a27a8a4dca97.png'" />
            <span class="username" style="margin-left: 10px;">{{ formatUsername('予我心安A3') }}</span>
        </el-menu-item>

    </el-menu>


    <!-- 嵌套路由视图，用于显示子组件 -->
    <div class="user-panel-container">
        <router-view />
    </div>


</template>

<script lang="ts" setup>
//官方引入
import router from '@/router'
import { ref } from 'vue'

//插件引入

//自定义引入

//资源引入


//数据
const activeIndex = ref('1')

const isDarkTheme = ref(false)

//方法
const handleSelect = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}

// 显示用户面板
const showUserPanel = () => {
    router.push('/home/user/userInfo')
}

// 切换主题的方法接口
const toggleTheme = () => {
    isDarkTheme.value = !isDarkTheme.value
    // TODO: 在这里添加实际的主题切换逻辑
    // 你可以在这里调用切换主题的函数或 emit 事件
    onThemeChange(isDarkTheme.value ? 'dark' : 'light')
}

// 主题切换接口，供外部实现具体逻辑
const onThemeChange = (theme: 'light' | 'dark') => {
    // 这里留一个接口方法，你可以在这里实现具体的主题切换逻辑
    console.log('Theme changed to:', theme)
    // 例如：
    // - 切换 CSS 类
    // - 调用 store 中的方法
    // - 修改 CSS 变量
    // - 调用 API 保存用户偏好
}

// 格式化用户名，超过8个字符显示省略号
const formatUsername = (username: string) => {
    if (username.length > 8) {
        return username.substring(0, 8) + '...'
    }
    return username
}

</script>

<style scoped>
.nav-box {
    background-color: white;
    padding: 0px 10px;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    margin: 10px 20px;
}

.nav-font {
    font-size: larger;
    color: #909399;
}

.nav-font-submenu {
    font-size: 21px;
    font-weight: 350;
}

.el-menu--horizontal {
    --el-menu-horizontal-height: 70px;
}

/*从语言选择开始居右 */
.el-menu--horizontal> :nth-child(7) {
    margin-right: auto;
}

/* 为独立的图标添加样式 */
.theme-toggle {
    height: var(--el-menu-horizontal-height);
    display: flex;
    align-items: center;
    padding: 0 20px;
    cursor: pointer;
    transition: background-color 0.3s;
    color: #909399;
    font-size: larger;
}

/* .theme-toggle:hover {
    background-color: var(--el-menu-hover-bg-color);
    color: var(--el-menu-hover-text-color);
} */

/* 用户面板容器样式 */
.user-panel-container {
    position: relative;
    width: 100%;
}
</style>