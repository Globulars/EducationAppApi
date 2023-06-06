pipeline {
    agent any

    parameters {
        choice(name: 'PROJECT', choices: ['api','migration', 'service'], description: 'App to deploy (Default:api). Expected values: [api|migration|services]' )
        choice(name: 'DEPLOY_ENV', choices: ['auto', 'local','prod'], description: 'Deployment environment (Default:auto, without deploy). Expected values: [test|prod]')
        choice(name: 'CLEAN_NODE', choices: ['auto', 'no', 'yes'], description: 'Clean nodejs cache during build. Expected values: [no|yes]')

    }

  environment {
    MSBUILD = "C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe"
    NUGET = "C:\\Jenkins-Server\\nuget.exe"
    NUGET_LATEST = "C:\\Jenkins-Server\\nuget.exe"
    MSDEPLOY = "C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe"
    CONFIG = 'Release'    
    BUILD_NAME = "${env.BRANCH_NAME.replaceAll('/', '-')}-${env.GIT_COMMIT}-${env.BUILD_NUMBER}"
    WIN_SCP = "c:\\winscp_installed\\winscp.com"    

  }
  stages {

    stage('Version') {
      when { anyOf {
        expression { return params.DEPLOY_ENV != 'auto' }
      }}
      steps {
        script {
          echo "bumping commit version"                      
          //bat "sed -i 's/@BUILD_NAME@/${BUILD_NAME}/g' ${WORKSPACE}\\Helpers\\Constants.cs"
          //bat "sed -i 's/@BUILD_NAME@/${BUILD_NAME}/g' ${WORKSPACE}\\Helpers\\Constants.cs"
          //bat "sed -i 's/@BUILD_NAME@/${BUILD_NAME}/g' ${WORKSPACE}\\Helpers\\Constants.cs"
        }
      }
    }


    stage('Build') {
      steps {
        script {
            if (params.PROJECT == 'api') {                
                bat """
                  \"${NUGET_LATEST}\" restore TPPortal.sln               
                """
                //  /p:Configuration=${env.CONFIG}
                bat "\"${MSBUILD}\" TPPortal.Api\\TPPortal.Api.csproj"    
            } else if (params.PROJECT == 'service') {                
                bat """
                  \"${NUGET_LATEST}\" restore TPPortal.sln               
                """
                //  /p:Configuration=${env.CONFIG}
                bat "\"${MSBUILD}\" TPPortal.BackgroundProcessor\\TPPortal.BackgroundProcessor.csproj"    
            }
        }
      }
    }

    stage('Deploy') {
        steps {
            script {

                if (params.PROJECT == 'api') {

                      if (params.DEPLOY_ENV != 'auto') {
                        bat """                    
                          echo \"updating config file for ${params.PROJECT} and env ${params.DEPLOY_ENV}\"      
                          copy /y deploy\\${params.PROJECT}\\${params.DEPLOY_ENV}\\Web.config TPPortal.Api\\Web.config
                        """
                      }

                  if (params.DEPLOY_ENV == 'test') {
                      // bat "\"${MSBUILD}\" TPPortal.Api\\TPPortal.Api.csproj /p:Configuration=${env.CONFIG}  /p:PublishProfile=iis /p:DeployOnBuild=true   /P:AllowUntrustedCertificate=true  /p:WebPublishMethod=MSDeploy /p:MSDeployPublishMethod=WMSVC /P:UserName=SERVER-PC\\Admin  /P:Password=XXXX /P:MsDeployServiceUrl=localhost /P:DeployIISAppPath=\"Default Web Site/cogsapi\" "
                  }
                } else if (params.PROJECT == 'migration') {
                    //TPPortal.Migration\App\TPPortal.Migration.exe sa sa
                } else if (params.PROJECT == 'service') {
                    if (params.DEPLOY_ENV == 'prod') {

                        bat """                    
                            \"${MSBUILD}\" TPPortal.sln /t:Build /p:Configuration=Release > NULL
                            exit 0
                        """
                        bat """
                            del /f TPPortal.BackgroundProcessor\\bin\\Release\\TPPortal.BackgroundProcess.exe.config
                            IF NOT EXIST "C:\\Cogs Prod\\BackGroundProcessor\\" md "C:\\Cogs Prod\\BackGroundProcessor\\"                            
                            xcopy /y /s /h /i /c /k /e /r  TPPortal.BackgroundProcessor\\bin\\Release "C:\\Cogs Prod\\BackGroundProcessor"
                        """                        
                  } else if (params.DEPLOY_ENV == 'local') {

                        bat """
                            C:\\Cogs Prod\\BackGroundProcessor\\TPPortal.BackgroundProcess.exe
                        """                        
                  }
                }
                
            }            
        }
    }
     stage('Verify') {
        steps {
            script {
              if (params.PROJECT == 'api') {
                if (params.DEPLOY_ENV == 'test') {
                  bat "curl -v http://localhost/cogsapi/api/health"

                }
              }
            }
        }
     }
  }

  post {
        cleanup {
            echo 'cleanup'
            /* deleteDir() clean up our workspace */
        }
    }
}
