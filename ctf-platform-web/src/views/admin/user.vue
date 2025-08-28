<template>
    <el-row class="showPanel">
        <el-col :span="4">
            <el-menu default-active="2" class="el-menu-vertical-demo card-box">

                <el-menu-item index="1">
                    <el-icon>
                        <User />
                    </el-icon>
                    <span>Navigator One</span>
                </el-menu-item>

                <el-menu-item index="2">
                    <el-icon>
                        <icon-menu />
                    </el-icon>
                    <span>Navigator Two</span>
                </el-menu-item>

                <el-menu-item index="3">
                    <el-icon>
                        <document />
                    </el-icon>
                    <span>Navigator Three</span>
                </el-menu-item>

                <el-menu-item index="4">
                    <el-icon>
                        <setting />
                    </el-icon>
                    <span>Navigator Four</span>
                </el-menu-item>

            </el-menu>
        </el-col>

        <el-col :span="20">
            <UserInfo style="margin-left: 20px;"></UserInfo>
        </el-col>

    </el-row>


</template>





<script setup lang="ts">
import UserInfo from '@/components/user/userInfo.vue';


</script>

<style scoped>
.showPanel {
    padding: 40px 17px;
    border-radius: 10px;
    background-color: red;
    margin: 10px 20px;
}
</style>