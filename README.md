# Glassdoor
![image](https://github.com/makhammadsoliyev/Glassdoor/assets/149594973/1d799987-643b-44aa-9674-d7a0269c80f3)
<pre>
UserService 
  1. RegisterAsync(user)
  2. LogInAsync(phone, password)
  3. GetAllAsync()
  4. GetByIdAsync(id)
  5. DeleteAsync(id)
  6. UpdateAsync(id, user)
  7. GetAllApplications(id)

CompanyService 
  1. RegisterAsync(company)
  2. LogInAsync(phone, password)
  3. GetAllAsync()
  4. GetByIdAsync(id)
  5. DeleteAsync(id)
  6. UpdateAsync(id, company)
  7. GetAllApplications(id)

JobService
  1. CreateAsync(job)
  2. CloseAsync(id) // make vacancy status close
  3. DeleteAsync(id)
  4. GetAllAsync()
  5. GetByIdAsync(id)

ApplicationService
  1. CreateAsync(application)
  2. DeleteAsync(id)
  3. GetAllAsync()
  4. GetByIdAsyncAsync(id)
</pre>
