<style>
/* 分栏布局 */
.resume-container {
    display: flex;
    width: 100%;
    gap: 20px;
  }
  
.container.reversed {
    display: flex;
    width: 100%;
    gap: 20px;
    margin-top: 200px;
}

  .main-content {
    flex: 7;  /* 70% */
    padding-right: 15px;
  }
  
  .secondMain{
       flex: 7;  /* 30% */
        padding-right: 1px;
        margin-left: 20px;
  }


  .sidebar {
      flex: 3;  /* 30% */
    border-left: 1px solid #eee;
    padding-left: 15px;
  }
  
  .girlfriend
  {
    flex: 3;  /* 70% */
    border-right: 1px solid #eee;
    padding-left: 15px;
    margin-top: 40px;
  }

  /* 强制分页 */
  .page-break {
    page-break-after: always;
    height: 0;
    visibility: hidden;
  }
  
  /* 图片样式 */
  .sidebar img {
    border-radius: 50%;
    display: block;
    margin: 0 auto 15px;
  }
  
  /* 打印优化 */
  @media print {
    body {
      margin: 0;
      padding: 1cm;
      font-size: 12pt;
    }
    .page-break {
      margin-top: 2cm;
    }

}
</style>

<!-- 分栏布局 -->
<div class="resume-container">

  <!-- 左侧栏（70%） -->
  <div class="main-content">
  
## 项目经历
### 电商平台 | 2023-2024
- 技术栈: Vue3 + TypeScript
- 实现商品秒杀功能，QPS提升300%

### 后台管理系统 | 2022-2023
- 技术栈: React + Node.js
- 开发权限管理模块

  </div>

  <!-- 右侧边栏（30%） -->
  <div class="sidebar">

![个人照片](gougou.png)

**基本信息**  
📧 john@example.com  
📱 138-0013-8000  

**技术栈**  
- JavaScript ★★★★★  
- Python ★★★☆☆  

  </div>
</div>

<!-- 强制分页 -->
<div class="page-break"></div>

<div class="container reversed">
    <div class="girlfriend">

**交往过的女朋友**
- 🐇小白兔
- 🦆黄小鸭
-  🕷猪猪侠
- 😴蟑螂宝宝
    </div>
    <div class="secondMain">

## 其他信息
### 教育背景
- XX大学 计算机科学 2018-2022

### 兴趣爱好
- 开源项目贡献
- 技术博客写作

    </div>
</div>