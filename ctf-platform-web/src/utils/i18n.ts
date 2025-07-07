import en from '@/locale-language/en.json';
import zh from '@/locale-language/zh.json';
import { createI18n } from 'vue-i18n';

const messages = {
    en,
    zh
};

const i18n = createI18n({
    legacy: false, // 使用 Composition API 模式
    locale: 'zh', // 设置默认语言
    fallbackLocale: 'en', // 当前语言没有对应翻译时使用的备用语言
    messages // 注入语言包
});

export default i18n;