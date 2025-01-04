<template>
    <!-- 展开用户信息按钮 -->
    <Transition name="button-transition" style="position: absolute; right: 0;">
        <el-button @click="show = !show" v-show="show" class="layer-panel-button">
            <el-icon>
                <CaretLeft />
            </el-icon>
        </el-button>
    </Transition>
    <!-- 用户信息 -->
    <el-row style="background-color: #F5F7FA;">
        <!-- 关闭用户信息按钮 -->
        <Transition name="collapse-button-transition">
            <el-button @click="show = !show" v-if="!show" class="layer-panel-collapse-button"
                style="width: 10%;"><el-icon>
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
    <el-drawer v-model="drawer" title="I am the title" :before-close="handleClose">
        <span>Hi, there!</span>
    </el-drawer>

</template>

<script lang="js">
import { ref, onMounted } from 'vue'

export default {
    setup() {
        const drawer = ref(false)
        const show = ref(true)
        //没头像，加载默认图标
        const defaultImageUrl = ref('https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png')
        const circleUrl = ref('https://img1.baidu.com/it/u=1641206708,3419329057&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500')
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
        return {
            drawer,
            show,
            circleUrl,
            handleClose
        }
    }
}
</script>

<style lang="scss" scoped>
/*
用户信息展开
*/
.slide-fade-enter-active {
    transition: all 0.1S ease-out;
}

.slide-fade-leave-active {
    transition: all 0.1S cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
    transform: translateX(100px);
    opacity: 0;
}

/*用户信息展开操作按钮动画*/
.button-transition-enter-active,
.button-transition-leave-active {
    transition: all 0.5s ease;
}

.button-transition-leave-to,
.button-transition-enter-from {
    transform: translateX(-100px);

}

.collapse-button-transition-enter-active {
    transition: all 0.5s ease-in-out;
}

.collapse-button-transition-leave-to,
.collapse-button-transition-enter-from {
    opacity: 0;
}

/* 用户信息展开按钮样式 */
.layer-panel-button {
    color: aqua;
    background-color: #F5F7FA;
    height: 120px;
    border-radius: 0;
}

.layer-panel-collapse-button {
    color: aqua;
    background-color: #F5F7FA;
    height: 120px;
    border-radius: 0;
}
</style>