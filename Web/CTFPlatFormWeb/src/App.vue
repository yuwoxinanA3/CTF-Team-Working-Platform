<template>

    <div style="background-color: #F5F7FA;width: 120px;opacity: 0.9;">
        <el-row style="height: 40px;display: flex;flex-direction: column;justify-content: center;padding: 0 5px ;">
            <el-avatar :size="30" :src="circleUrl" />
            <span>予我心安</span>
        </el-row>
        <el-row>
            <el-button type="primary" style="width: 100%;height: 10px;border-radius: 0;border: none;font-size: 10px;"
                @click="drawer = true" plain>
                登录
            </el-button>
        </el-row>
    </div>



    <el-drawer v-model="drawer" title="I am the title" :direction="direction" :before-close="handleClose">
        <span>Hi, there!</span>
    </el-drawer>

</template>

<script lang="js">
import { ref, onMounted } from 'vue'

export default {
    setup() {
        const drawer = ref(false)
        //没头像，加载默认图标
        const defaultImageUrl = 'https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png'
        const circleUrl = 'https://img1.baidu.com/it/u=1641206708,3419329057&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500'

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
            circleUrl,
            handleClose
        }
    }
}
</script>

<style lang="scss" scoped>
/* 你的样式代码 */
</style>