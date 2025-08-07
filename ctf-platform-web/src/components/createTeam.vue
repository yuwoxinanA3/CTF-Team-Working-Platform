<template>
    <h1 style="text-align: center;width: 100%;">创建战队</h1>

    <el-form ref="ruleFormRef" style="max-width: 100%;background-color: #ffffff;margin-top: 30px;padding: 40px 17px;"
        :model="ruleForm" status-icon :rules="rules" label-width="auto" class="demo-ruleForm">

        <el-form-item label="战队名称" prop="teamName">
            <el-input v-model="ruleForm.teamName" style="max-width: 600px" placeholder="请输入战队名称">
                <template #append>
                    <el-button type="primary" @click="checkTeamNameAvailability(ruleForm.teamName)"
                        :loading="isChecking" :disabled="!ruleForm.teamName || ruleForm.teamName.trim() === ''">
                        检验可用性
                    </el-button>
                </template>
            </el-input>
            <!-- 添加状态提示 -->
            <div v-if="isAvailable !== null && lastCheckedName === ruleForm.teamName" style="margin-top: 5px;">
                <el-text v-if="isAvailable" type="success">✓ 名称可用</el-text>
                <el-text v-else type="danger">✗ 名称已被占用</el-text>
            </div>
        </el-form-item>

        <el-form-item>
            <div style="display: flex;justify-content: center;width: 100%;">
                <el-button type="primary" @click="submitForm(ruleFormRef)" style="width: 70%;" :loading="false"
                    :disabled="isAvailable === false || !ruleForm.teamName || ruleForm.teamName.trim() === ''">
                    创建战队
                </el-button>
            </div>
        </el-form-item>
    </el-form>
</template>

<script lang="ts" setup>
//官方引入
import { reactive, ref, watch } from 'vue'
//插件引入
import axios from 'axios'
import type { FormInstance, FormRules } from 'element-plus'
//自定义引入
import type { text_req } from '@/api-services/models/text_req'
//资源引入


//数据
const ruleFormRef = ref<FormInstance>()

const ruleForm = reactive({
    teamName: '',
})

//状态变量
const isChecking = ref(false) // 正在检查中
const isAvailable = ref<boolean | null>(null) // null: 未检查, true: 可用, false: 不可用
const lastCheckedName = ref('') // 上次检查的名称

//表单规则
const rules = reactive<FormRules<typeof ruleForm>>({
    teamName: [
        { required: true, message: '请输入战队名称', trigger: 'blur' },
        { min: 1, max: 20, message: '长度在 1 到 20 个字符', trigger: 'blur' },
        {
            validator: (_rule, value, callback) => {
                if (value && isAvailable.value === false && lastCheckedName.value === value) {
                    callback(new Error('战队名称已被占用'))
                } else {
                    callback()
                }
            },
            trigger: 'blur'
        }
    ]
})

//方法
// 检查战队名称是否可用的函数
const checkTeamNameAvailability = async (name: string) => {
    // 允许单字名称，只需非空检查
    if (!name || name.trim() === '') return

    isChecking.value = true
    lastCheckedName.value = name

    try {
        // 构造符合后端 TextReq 格式的请求体
        const teamName: text_req = {
            TextContent: name
        };
        // 发送验证请求
        const response = await axios.post('/api/API/Team/IsAvailableTeamName', teamName);

        if (response.data) {
            isAvailable.value = true
            ruleFormRef.value?.validateField('teamName')
        }
        else
            isAvailable.value = false

    } catch (error) {
        isAvailable.value = false
        console.error('检查名称可用性时出错:', error)
    } finally {
        isChecking.value = false
    }
}


const submitForm = async (formEl: FormInstance | undefined) => {
    if (!formEl) return

    // 先触发表单验证
    const valid = await formEl.validate().catch(() => false)

    if (valid) {
        // 检查是否已经验证过名称且名称可用
        if (isAvailable.value === true && lastCheckedName.value === ruleForm.teamName) {
            console.log('submit!', ruleForm.teamName)
            // 这里执行实际的创建战队逻辑
            // await createTeam(ruleForm.teamName)
        } else {
            // 如果还没有检查名称或名称已更改，先检查名称
            await checkTeamNameAvailability(ruleForm.teamName)

            // 再次检查验证结果
            if (isAvailable.value === true) {
                console.log('submit!', ruleForm.teamName)
                // 这里执行实际的创建战队逻辑
                // await createTeam(ruleForm.teamName)
            } else {
                console.log('战队名称不可用，无法提交')
            }
        }
    } else {
        console.log('error submit!')
    }
}




//监听

// 监听战队名称变化，重置验证状态
watch(() => ruleForm.teamName, (newName, _oldName) => {
    // 支持单字，只在名称真正改变时重置状态
    if (newName !== lastCheckedName.value) {
        isAvailable.value = null
    }
})


</script>