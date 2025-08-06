<template>
    <h1 style="text-align: center;width: 100%;">创建战队</h1>

    <el-form ref="ruleFormRef" style="max-width: 100%;background-color: #ffffff;margin-top: 30px;padding: 40px 17px;"
        :model="ruleForm" status-icon :rules="rules" label-width="auto" class="demo-ruleForm">

        <el-form-item label="战队名称" prop="teamName">
            <el-input v-model="ruleForm.teamName" style="max-width: 600px" placeholder="请输入战队名称">
                <template #append>
                    <el-button type="primary" @click="checkTeamNameAvailability(ruleForm.teamName)"
                        :loading="isChecking" :disabled="!ruleForm.teamName || ruleForm.teamName.length < 2">
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
                    :disabled="isAvailable === false">
                    创建战队
                </el-button>
            </div>
        </el-form-item>
    </el-form>
</template>

<script lang="ts" setup>
import { reactive, ref, watch } from 'vue'
import type { FormInstance, FormRules } from 'element-plus'

const ruleFormRef = ref<FormInstance>()

const ruleForm = reactive({
    teamName: '',
})

// 添加这些状态变量
const isChecking = ref(false) // 正在检查中
const isAvailable = ref<boolean | null>(null) // null: 未检查, true: 可用, false: 不可用
const lastCheckedName = ref('') // 上次检查的名称

//定义表单规则
const rules = reactive<FormRules<typeof ruleForm>>({
    teamName: [
        { required: true, message: '请输入战队名称', trigger: 'blur' },
        { min: 2, max: 20, message: '长度在 2 到 20 个字符', trigger: 'blur' },
        {
            validator: (rule, value, callback) => {
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


// 检查战队名称是否可用的函数
const checkTeamNameAvailability = async (name: string) => {
    if (!name || name.length < 2) return

    isChecking.value = true
    lastCheckedName.value = name

    try {
        // 这里应该调用实际的API检查名称是否被占用
        // 示例：const response = await api.checkTeamName(name)
        // 模拟API调用
        await new Promise(resolve => setTimeout(resolve, 1000))

        // 模拟检查结果（实际应该根据API返回结果判断）
        const isNameAvailable = Math.random() > 0.5 // 随机模拟结果
        isAvailable.value = isNameAvailable

        // 如果名称不可用，添加表单错误
        if (!isNameAvailable) {
            ruleFormRef.value?.validateField('teamName')
        }
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

// 监听战队名称变化，重置验证状态
watch(() => ruleForm.teamName, (newName, oldName) => {
    if (newName !== lastCheckedName.value) {
        isAvailable.value = null
    }
})






</script>