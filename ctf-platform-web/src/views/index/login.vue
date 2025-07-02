<template>
    <div style="display: flex;justify-content: center;">
        <!-- 左侧巨石阵显示加盟战队 -->
        <div id="abiltily_wrapper_box" style="width: 75%;">
            <div class="abiltily_wrapper">
                <div class="abiltily_inner" style="--quantity: 10;">
                    <div class="abiltily_card" style="--index: 0;--color-card: 142, 249, 252;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png' /></div>
                    </div>
                    <div class="abiltily_card" style="--index: 1;--color-card: 142, 252, 204;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 2;--color-card: 142, 252, 157;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 3;--color-card: 215, 252, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 4;--color-card: 252, 252, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 5;--color-card: 252, 208, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 6;--color-card: 252, 142, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 7;--color-card: 252, 142, 239;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 8;--color-card: 204, 142, 252;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 9;--color-card: 142, 202, 252;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                </div>
            </div>
        </div>


        <!-- 登录组件框 -->
        <div style="width: 25%;background-color: white;">
            <loginForm />
        </div>

    </div>


</template>


<script setup lang="ts">
import loginForm from '@/components/loginForm.vue'

</script>


<style>
/* 全局样式 */
body {
    background-color: #1d1c1c;
}
</style>


<style scoped>
/* 功能巨石阵样式 */
#abiltily_wrapper_box {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    overflow: hidden;
}

.abiltily_wrapper {
    width: 100%;
    height: 130%;
    position: relative;
    text-align: center;
    display: flex;
    align-items: center;
    justify-content: center;
    overflow: hidden;
}

.abiltily_inner {
    --w: 200px;
    --h: 300px;
    --translateZ: calc((var(--w) + var(--h)) + 0px);
    --rotateX: -15deg;
    /*仰角*/
    --perspective: 1000px;
    position: absolute;
    width: var(--w);
    height: var(--h);
    top: 25%;
    left: calc(50% - (var(--w) / 2) - 2.5px);
    z-index: 2;
    transform-style: preserve-3d;
    transform: perspective(var(--perspective));
    animation: abiltily_rotating 20s linear infinite;
}

@keyframes abiltily_rotating {
    from {
        transform: perspective(var(--perspective)) rotateX(var(--rotateX))rotateY(0);
    }

    to {
        transform: perspective(var(--perspective)) rotateX(var(--rotateX))rotateY(1turn);
    }
}

.abiltily_card {
    position: absolute;
    -webkit-box-reflect: below 10px linear-gradient(transparent, rgba(0, 0, 0, 0.5));
    border-radius: 12px;
    overflow: hidden;
    inset: 0;
    transform: rotateY(calc((360deg / var(--quantity)) * var(--index)))translateZ(var(--translateZ));
}

.abiltily_img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transform: rotateY(-1);
}

img {
    width: 100%;
}
</style>