# Git推送指令

## 成功的推送命令

### 基础推送命令
```bash
# 1. 更新远程URL（包含token）
git remote set-url origin https://ghp_cCvW4GZGZGSwNT95SJK0N7NIijTfQfAT0od10n@github.com/gx2023/EMU-DigitalTwin-Sim.git

# 2. 推送代码
git push origin main
```

### 完整推送流程
```bash
# 1. 检查状态
git status

# 2. 添加更改（如果有）
git add .

# 3. 提交更改
git commit -m "描述性的提交消息"

# 4. 更新远程URL（确保包含token）
git remote set-url origin https://ghp_cCvW4GZGZGSwNT95SJK0N7NIijTfQfAT0od10n@github.com/gx2023/EMU-DigitalTwin-Sim.git

# 5. 推送代码
git push origin main
```

## 故障排除

### 常见问题及解决方案

1. **认证失败**
   - 确保token正确
   - 重新设置远程URL
   - 检查网络连接

2. **网络超时**
   - 检查网络连接
   - 尝试多次推送
   - 确保GitHub服务器可访问

3. **分支问题**
   - 确保在正确的分支上
   - 检查本地分支与远程分支的关系

## 注意事项

- **Token安全**：不要在公开场合分享token
- **定期更新**：GitHub token可能会过期，需要定期更新
- **权限设置**：确保token有推送权限
- **备份**：保留token的安全备份

## 参考信息

- **仓库地址**：https://github.com/gx2023/EMU-DigitalTwin-Sim
- **分支**：main
- **Token**：ghp_cCvW4GZGZGSwNT95SJK0N7NIijTfQfAT0od10n
