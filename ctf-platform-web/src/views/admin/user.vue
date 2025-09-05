<template>
    <el-row class="showPanel">
        <el-col :span="2">
            <el-menu default-active="1" class="el-menu-vertical-demo card-box" style="padding: 20px 10px 50px 10px">

                <el-menu-item index="1" class="sub-nav-font" @click="showUserPanel">
                    <el-icon :size="18">
                        <User />
                    </el-icon>
                    <span>个人主页</span>
                </el-menu-item>

                <el-menu-item index="2" class="sub-nav-font" @click="showChangePwdPanel">
                    <el-icon :size="18">
                        <Key />
                    </el-icon>
                    <span>修改密码</span>
                </el-menu-item>

            </el-menu>
        </el-col>

        <el-col :span="22">
            <router-view style="margin-left: 20px;" />
        </el-col>

    </el-row>


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
    padding: 40px 17px;
    border-radius: 10px;
    margin: 10px 20px;
}

.sub-nav-font {
    font-size: 18px;
    border-radius: 10px;
    justify-content: center;
}
</style>