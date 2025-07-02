<template>
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
                            <el-form ref="formRef" :model="form" label-width="auto">

                                <el-form-item label="用户名" prop="username" label-position="right"
                                    class="el_form_item_login">
                                    <el-input v-model="form.username"></el-input>
                                </el-form-item>

                                <el-form-item label="密码" prop="password" label-position="right"
                                    class="el_form_item_login">
                                    <el-input type="password" v-model="form.password"></el-input>
                                </el-form-item>

                                <el-form-item class="el_form_item_login">
                                    <div style="display: flex;justify-content: space-between;width: 100%;">
                                        <div>
                                            自动登录
                                        </div>
                                        <div>
                                            <span id="register" @click="handleRegisterClick">注册</span>
                                            忘记密码
                                        </div>
                                    </div>

                                </el-form-item>

                                <el-form-item class="el_form_item_login">



                                    <div style="line-height: 22px;margin:0 0 8px 0;color: #9b9b9b;">
                                        <span style="vertical-align:middle">第三方登录:</span>
                                        <img :src="qqIcon" width="22" height="22"
                                            style="vertical-align:middle;margin-left: 8px" title="QQ">
                                        <img :src="zhifubaoIcon" width="22" height="22"
                                            style="vertical-align:middle;margin-left: 8px" title="支付宝">
                                        <img :src="giteeIcon" width="22" height="22"
                                            style="vertical-align:middle;margin-left: 8px" title="Gitee">
                                        <img :src="githubIcon" width="22" height="22"
                                            style="vertical-align:middle;margin-left: 8px" title="GitHub">
                                    </div>

                                    <div id="login_button_box">
                                        <el-button id="login_button" type="primary" @click="submitForm">登录</el-button>
                                        <el-button id="reset_button" type="info" plain>重置</el-button>
                                    </div>
                                </el-form-item>

                            </el-form>

                        </el-tab-pane>

                        <!-- 手机号登录 -->
                        <el-tab-pane label="手机号登录" name="second">暂未接入</el-tab-pane>
                    </el-tabs>
                </div>
            </div>

        </div>
    </div>

</template>

<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios';
import logo from '@/assets/images/logo.png'
import qqIcon from '@/assets/images/qq.png'
import zhifubaoIcon from '@/assets/images/支付宝.png'
import giteeIcon from '@/assets/images/gitee.png'
import githubIcon from '@/assets/images/github.png'


import { ElMessage, type ElForm } from 'element-plus';





//数据
const activeName = ref('first')
const form = ref({
    username: '',
    password: ''
});
const formRef = ref<InstanceType<typeof ElForm> | null>(null);

//方法
function handleClick(tab: any, event: any) {
    console.log(tab, event)
}

const submitForm = async () => {
    if (!formRef.value) return;

    try {
        const valid = await formRef.value.validate();
        if (valid) {
            // 表单验证通过，发送登录请求
            const formData = form.value;
            await axios.post('/api/login', formData);
            console.log('登录成功');
            // 登录成功后跳转或处理 token 等操作
        }
    } catch (error) {
        console.error('登录失败:', error);
        ElMessage.error('登录失败，请检查用户名或密码');
    }
};

const handleRegisterClick = () => {
    // 示例：跳转到注册页面
    window.location.href = '/register'; // 或者使用 router.push('/register') 如果你用 Vue Router
};


//监听



</script>


<style scoped>
#login_box {
    text-align: center;
}

#login_box_title_logo_box {
    display: flex;
    justify-content: center;
}

#login_box_title_logo_box_padding {
    width: 80%;
    display: flex;
    justify-content: center;
}

#login_box_title_text {
    margin-top: 20px;
    font-size: xx-large;
    font-weight: 400;
    color: rgb(120, 120, 117);
}


#login_box_content {
    /* background-color: red; */
    margin-top: 50px;
}

#login_box_content_box {
    /* background-color: rgb(13, 255, 0); */
    display: flex;
    justify-content: center;
}

.demo-tabs>.el-tabs__content {
    padding: 32px;
    color: #6b778c;
    font-size: 32px;
    font-weight: 600;
}


.el_form_item_login {
    margin-top: 30px;
}


#register {
    color: #409EFF;
    /* 使用 Element Plus 主色调 */
    cursor: pointer;
    text-decoration: none;
}

#register:hover {
    color: #337ecc;
    /* 悬停时变深色 */
    text-decoration: underline;
    /* 添加下划线 */
}

#third_party_login .el-button {
    margin: 0 10px;
}

#login_button_box {
    width: 100%;
    display: flex;
    justify-content: space-between;
    padding: 0 30px;
}

#login_button {
    width: 30%;
}

#reset_button {
    width: 30%;
}
</style>