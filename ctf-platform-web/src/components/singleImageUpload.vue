<template>
    <div class="single-image-upload-wrapper">
        <div class="user-info-head" @click="editCropper()">
            <img :src="value || defaultAvatar" title="点击上传头像" class="img-circle img-lg avatar-preview" />

            <el-dialog :title="title" v-model="open" width="800px" append-to-body @opened="modalOpened"
                @close="closeDialog">
                <el-row>
                    <el-col :xs="24" :md="12" :style="{ height: '350px' }">
                        <vue-cropper ref="cropper" :img="options.img" :info="true" :autoCrop="options.autoCrop"
                            :autoCropWidth="options.autoCropWidth" :autoCropHeight="options.autoCropHeight"
                            :fixedBox="options.fixedBox" :outputType="options.outputType" @realTime="realTime"
                            v-if="visible" />
                    </el-col>
                    <!-- 右侧预览 -->
                    <el-col :xs="24" :md="12" :style="{ height: '350px' }">
                        <div class="avatar-upload-preview">
                            <div class="preview-container" :style="options.previews.div">
                                <img :src="previewUrl" :style="options.previews.img" />
                            </div>
                        </div>
                    </el-col>
                </el-row>
                <br />
                <el-row>
                    <el-col :lg="2" :md="2">
                        <el-upload action="#" :http-request="requestUpload" :show-file-list="false"
                            :before-upload="beforeUpload">
                            <el-button>
                                选择
                                <el-icon class="el-icon--right">
                                    <Upload />
                                </el-icon>
                            </el-button>
                        </el-upload>
                    </el-col>
                    <el-col :lg="{ span: 1, offset: 2 }" :md="2">
                        <el-button icon="Plus" @click="changeScale(1)"></el-button>
                    </el-col>
                    <el-col :lg="{ span: 1, offset: 1 }" :md="2">
                        <el-button icon="Minus" @click="changeScale(-1)"></el-button>
                    </el-col>
                    <el-col :lg="{ span: 1, offset: 1 }" :md="2">
                        <el-button icon="RefreshLeft" @click="rotateLeft()"></el-button>
                    </el-col>
                    <el-col :lg="{ span: 1, offset: 1 }" :md="2">
                        <el-button icon="RefreshRight" @click="rotateRight()"></el-button>
                    </el-col>
                    <el-col :lg="{ span: 2, offset: 6 }" :md="2">
                        <el-button type="primary" @click="uploadImg()">提 交</el-button>
                    </el-col>
                </el-row>
            </el-dialog>
        </div>
    </div>
</template>

<script setup>
//官方引入
import { ref, reactive, computed, nextTick } from 'vue'
//插件引入
import { VueCropper } from "vue-cropper";
import "vue-cropper/dist/index.css";
import axios from 'axios'
// 定义组件属性
const props = defineProps({
    // 当前图片URL
    value: {
        type: String,
        default: ''
    },
    // 默认头像
    defaultAvatar: {
        type: String,
        default: 'https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png'
    },
    // 标题
    title: {
        type: String,
        default: '修改图片'
    }
})

// 定义事件
const emit = defineEmits(['input', 'success', 'error'])

// 数据
const open = ref(false)
const visible = ref(false)
const cropper = ref(null)

//图片裁剪数据
const options = reactive({
    img: '', // 裁剪图片的地址
    autoCrop: true, // 是否默认生成截图框
    autoCropWidth: 200, // 默认生成截图框宽度
    autoCropHeight: 200, // 默认生成截图框高度
    fixedBox: true, // 固定截图框大小 不允许改变
    outputType: "png", // 默认生成截图为PNG格式
    previews: {} //预览数据
})

// 计算属性
const previewUrl = computed(() => {
    return options.previews.url || props.value || props.defaultAvatar;
});

// 方法
/** 编辑头像 */
function editCropper() {
    options.img = props.value || props.defaultAvatar
    open.value = true
}

/** 实时预览 */
function realTime(data) {
    options.previews = data;
}

/** 打开弹出层结束时的回调 */
function modalOpened() {
    // 使用 setTimeout 替代 nextTick 来避免渲染问题
    setTimeout(() => {
        visible.value = true
    }, 100)
}

/** 关闭窗口 */
function closeDialog() {
    visible.value = false
    open.value = false
    options.img = ''
}

/** 覆盖默认上传行为 */
function requestUpload() { }

/** 上传预处理 */
function beforeUpload(file) {
    if (file.type.indexOf("image/") === -1) {
        alert("文件格式错误，请上传图片类型,如：JPG，PNG后缀的文件。")
        return false
    } else {
        const reader = new FileReader()
        reader.readAsDataURL(file)
        reader.onload = () => {
            options.img = reader.result
        }
        return false // 阻止默认上传行为
    }
}

/** 图片缩放 */
function changeScale(num) {
    num = num || 1
    cropper.value?.changeScale(num)
}

/** 向左旋转 */
function rotateLeft() {
    cropper.value?.rotateLeft()
}

/** 向右旋转 */
function rotateRight() {
    cropper.value?.rotateRight()
}

// 压缩图片
function compress(img) {
    let canvas = document.createElement("canvas")
    let ctx = canvas.getContext("2d")
    let width = img.width
    let height = img.height
    canvas.width = width
    canvas.height = height
    // 铺底色
    ctx.fillStyle = "#fff"
    ctx.fillRect(0, 0, canvas.width, canvas.height)
    ctx.drawImage(img, 0, 0, width, height)
    // 进行压缩
    let ndata = canvas.toDataURL("image/jpeg", 0.8)
    return ndata
}

/** 上传图片 */
function uploadImg() {
    // 获取截图的 base64 数据
    cropper.value?.getCropData((data) => {
        let img = new Image()
        img.src = data
        img.onload = async () => {
            let _data = compress(img)

            // 将base64转换为Blob
            const blob = await fetch(_data).then(res => res.blob())
            const formData = new FormData()
            formData.append('avatar', blob, 'avatar.png')
            // 可根据需要切换上传类型: local 或 cloud
            formData.append('uploadType', 'local')

            try {
                // 直接调用上传接口，不需要通过props传递
                const response = await axios.post('/api/API/Upload/UploadAvatar/avatar', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                })
                // 后端基础URL (根据你的代理配置，应该使用这个地址来访问后端资源)
                const BACKEND_BASE_URL = 'http://localhost:5193';
                // 返回上传后的URL
                const url = response.data?.avatar || response.data?.url || response.data
                let fullUrl = url;
                if (url && url.startsWith('/')) {
                    // 如果是相对路径，则添加基础URL
                    fullUrl = `${BACKEND_BASE_URL}${url}`;
                }

                emit('success', fullUrl)
                emit('input', fullUrl)

                open.value = false
                visible.value = false

                alert("上传成功")
            } catch (error) {
                emit('error', error)
                alert("上传失败")
            }
        }
    })
}

</script>

<style lang="scss" scoped>
.single-image-upload-wrapper {
    text-align: center;
    /* 确保子元素居中 */
    width: 100%;
}

.avatar-preview {
    height: 120px;
    border-radius: 50%;
}


.user-info-head {
    position: relative;
    display: inline-block;
    height: 120px;
}

.user-info-head:hover:after {
    content: "+";
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
    color: #eee;
    background: rgba(0, 0, 0, 0.5);
    font-size: 24px;
    font-style: normal;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    cursor: pointer;
    line-height: 110px;
    border-radius: 50%;
}

/* 添加以下样式以支持预览功能 */
.avatar-upload-preview {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100%;
    overflow: hidden;
}

.preview-container {
    overflow: hidden;
    border-radius: 50%;
    border: 1px solid #ccc;
}
</style>