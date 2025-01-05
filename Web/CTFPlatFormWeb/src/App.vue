<template>
    <!-- 展开用户信息按钮 -->
    <Transition name="button-transition" style="position: absolute; right: 0;">
        <el-button @click="show = !show" v-show="show" class="layer-panel-button">
            <el-icon :size="30">
                <CaretLeft />
            </el-icon>
        </el-button>
    </Transition>
    <!-- 用户信息 -->
    <el-row id="user_info">
        <!-- 关闭用户信息按钮 -->
        <Transition name="collapse-button-transition">
            <el-button @click="show = !show" v-if="!show" class="layer-panel-collapse-button"
                style="width: 10%;"><el-icon :size="30">
                    <CaretRight />
                </el-icon></el-button>
        </Transition>
        <!-- 用户信息面板 -->
        <Transition name="slide-fade">
            <div style="opacity: 0.9;width: 90%;" v-if="!show">
                <el-row
                    style="height: 80px;width: 60%;display: flex;flex-direction: column;justify-content: center;padding: 0 5px ;">
                    <el-avatar :size="60" :src="circleUrl" />
                    <div>
                        <span style="font-size: large;">Rotten</span>
                    </div>
                    <div>
                        <span style="font-size: x-large;">予我心安</span>
                    </div>
                </el-row>
                <el-row style="display: flex;justify-content: space-around;">
                    <el-button type="success" style="font-size: medium;" @click="drawer = true">
                        切换账号
                    </el-button>
                    <el-button type="danger" style="font-size: medium;">退出登录</el-button>
                </el-row>
            </div>
        </Transition>
    </el-row>

    <!-- 侧边展开的登录栏 -->
    <el-drawer v-model="drawer" :before-close="handleClose">

        <div id="login_box">
            <!-- 登录面板logo区 -->
            <div id="login_box_title">
                <div id="login_box_title_logo_box">

                    <div id="login_box_title_logo_box_padding">
                        <div style="">
                            <el-image style="width: 100px; height: 100px;" :src="logo" />
                        </div>

                        <div style="padding-left: 30px;">
                            <div style="line-height:100px;font-size: 90px;font-weight: 600;">Rotten</div>
                        </div>
                    </div>
                </div>
                <div id="login_box_title_text">
                    CTF团队协作平台
                </div>
            </div>
            <!-- 登录面板区 -->
            <div id="login_box_content">
                <div id="login_box_content_box">
                    <div style="width: 75%;">
                        <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick" :stretch="true">
                            <!-- 账号密码登录 -->
                            <el-tab-pane label="账号密码登录" name="first">

                            </el-tab-pane>
                            <!-- 手机号登录 -->
                            <el-tab-pane label="手机号登录" name="second">Config</el-tab-pane>
                        </el-tabs>
                    </div>
                </div>

            </div>
        </div>


    </el-drawer>

</template>

<script lang="js">
import { ref, onMounted } from 'vue'
import logo from '@/assets/images/logo.png'
export default {
    setup() {
        const drawer = ref(false)
        const show = ref(true)
        //没头像，加载默认图标
        const defaultImageUrl = ref('https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png')
        const circleUrl = ref('https://img1.baidu.com/it/u=1641206708,3419329057&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500')
        const activeName = ref('first')
        const handleClose = (done) => {
            done()
        }

        // 检查本地头像是否存在，如果不存在则使用默认图标
        onMounted(() => {
            const img = new Image()
            img.src = circleUrl.value
            img.onerror = () => {
                circleUrl.value = defaultImageUrl
            }
        })


        const handleClick = (tab, event) => {
            console.log(tab, event)
        }


        return {
            drawer,
            show,
            logo,
            activeName,
            circleUrl,
            handleClose
        }
    }
}
</script>

<style lang="scss" scoped>
@import '../src/assets/css/index.css';
@import '../src/assets/css/login.css';
</style>