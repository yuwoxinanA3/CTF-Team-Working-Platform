<template>
    <div class="showPanel">
        <div class="sidebar">
            <el-menu default-active="1" class="el-menu-vertical-demo card-box" style="padding: 20px 10px 50px 10px;">
                <el-menu-item index="1" class="sub-nav-font" @click="showUserPanel">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <User />
                    </el-icon>
                    <span>{{ $t('user.pseronPage') }}</span>
                </el-menu-item>

                <el-menu-item index="2" class="sub-nav-font" @click="showChangePwdPanel">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <Key />
                    </el-icon>
                    <span>{{ $t('user.changePwdPage') }}</span>
                </el-menu-item>
            </el-menu>
        </div>

        <div class="main-content">
            <router-view style="margin-left: 20px;" />
        </div>
    </div>
</template>





<script setup lang="ts">
import router from '@/router';


// 显示用户面板
const showUserPanel = () => {
    router.push('/home/user/userInfo')
}

const showChangePwdPanel = () => {
    router.push('/home/user/changePwd')
}




</script>

<style scoped>
.showPanel {
    display: flex;
    padding: 20px 0;
    margin: 10px 20px;
    background-color: none;
}

.sidebar {
    flex-shrink: 0;
    /* 不压缩 */
    width: auto;
    /* 自动宽度 */
    min-width: 120px;
    /* 最小宽度，防止太窄 */
    max-width: 200px;
    /* 防止过宽 */
    border-right: 1px solid #ebeef5;
}

.main-content {
    flex-grow: 1;
    /* 占据剩余空间 */
    border-radius: 20px;
}

.sub-nav-font {
    font-size: 16px;
    border-radius: 10px;
    justify-content: left;
}
</style>