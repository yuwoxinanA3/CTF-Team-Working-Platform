<template>
    <div class="not-found">
        <div class="background">
            <div class="bg-circle"></div>
            <div class="bg-circle"></div>
            <div class="bg-circle"></div>
        </div>

        <div class="content">
            <h1 class="error-code">404</h1>
            <p class="message">Oops! 页面找不到了。</p>
            <button class="back-home" @click="$router.push('/')">返回首页</button>
        </div>
    </div>
</template>

<script setup>
// 可以使用组合式 API 编写逻辑
</script>

<style scoped lang="scss">
@use 'sass:color';

$primary-color: #36184F;
$secondary-color: #FF9D87;
$text-color: white;

.not-found {
    position: relative;
    width: 100vw;
    height: 100vh;
    background: $primary-color;
    color: $text-color;
    display: flex;
    justify-content: center;
    align-items: center;
    overflow: hidden;
    font-family: 'Barlow', sans-serif;

    .background {
        position: absolute;
        width: 100%;
        height: 100%;

        .bg-circle {
            position: absolute;
            border-radius: 50%;
            background: radial-gradient(circle, rgba($secondary-color, 0.2) 0%, rgba($secondary-color, 0) 70%);
            width: 500px;
            height: 500px;
            animation: float 15s infinite ease-in-out;

            &:nth-child(1) {
                top: -100px;
                left: -100px;
                animation-delay: 0s;
            }

            &:nth-child(2) {
                bottom: -100px;
                right: -100px;
                animation-delay: 5s;
            }

            &:nth-child(3) {
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                width: 800px;
                height: 800px;
                animation-delay: 10s;
            }
        }
    }

    @keyframes float {

        0%,
        100% {
            transform: translate(0, 0);
        }

        50% {
            transform: translate(30px, 30px);
        }
    }

    .content {
        z-index: 1;
        text-align: center;
        max-width: 500px;
        padding: 20px;

        .error-code {
            font-size: 100px;
            margin-bottom: 20px;
            background: linear-gradient(90deg, #FFEDC0 0%, #FF9D87 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .message {
            font-size: 24px;
            margin-bottom: 30px;
            opacity: 0.8;
        }

        .back-home {
            padding: 12px 30px;
            border-radius: 50px;
            background: $secondary-color;
            color: white;
            font-weight: bold;
            border: none;
            cursor: pointer;
            transition: all 0.3s ease;

            &:hover {
                background: color.adjust($secondary-color, $lightness: +10%);
                transform: translateY(-3px);
                box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
            }
        }
    }
}
</style>